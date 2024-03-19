<template>
    <div class="container">
        <div class="searchInput">
           <input type="text" placeholder="search by id" @input="sortByName(value)" v-model="sortByNameText">
           <input type="text" placeholder="search by employee" @input="sortByLocation()" v-model="sortByLocationText">
        </div>
        <div class="headers">
            <button @click="sortBy('ID')">ID </button>
            <button @click="sortBy('EmployeeName')">Employee Name</button>
            <button> Comment </button>
            <button @click="sortBy('Status')">Status</button>
        </div>
        <div class="itemsDiv" v-if="isListShowing"> 
            <SingleRequest v-for="(item,index) in items" 
            :key="index"
            :item="item"
            :selected="index===selectedIndex" 
            @click="selectItem(index)"/>
        </div>
        <div class="itemsDiv" v-if="!isListShowing"> 
            <RequestDetails> </RequestDetails>
        </div>
    </div>
</template>

<script>
import TMARequest from '@/Data/TMARequest';
import axios from 'axios';
import SingleRequest from './SingleRequest.vue';
import RequestDetails from './RequestDetails.vue';

export default {
    data() {
        return {
            selectedIndex: null,
            items : [],
            previousColumn : 'ID',
            direction : 1,
            originalItemsArray : [],
            sortByNameText : '',
            sortByLocationText: '',
            isListShowing: true
        }
    },
    components: {
        SingleRequest,
        RequestDetails,
    },
    methods: {
        selectItem(index) {
            this.selectedIndex = index;
        },
        sortBy(column) {
            if(this.previousColumn == column) {
                this.direction *= -1;
            }
            this.previousColumn = column;
            if(this.items != null && this.items.length > 1) {
                switch (column) {
                    case 'ID':
                        this.items.sort((a, b) => (a.ItemID - b.ItemID) * this.direction);
                        break;
                    case 'EmployeeName':
                        this.items.sort((a, b) => ((a.ItemGroup || '').localeCompare(b.ItemGroup || '')) * this.direction);
                        break;
                    case 'Status':
                        this.items.sort((a, b) => ((a.UnitOfMeasurement || '').localeCompare(b.UnitOfMeasurement || '')) * this.direction);
                        break;
                    default:
                        this.items.sort((a, b) => a.ItemID - b.ItemID);
                        break;
                    }
                }
        },
        sortByName() {
            if(this.sortByNameText == '') {
                this.items = this.originalItemsArray;
                return;
            }
            let newTable = [];
            this.originalItemsArray.forEach(element => {
                if(element.ItemGroup.toUpperCase().startsWith(this.sortByNameText.toUpperCase())) {
                    newTable.push(element);
                }
            });
           this.items = newTable;      
        },
        sortByLocation() {
            if(this.sortByLocationText == '') {
                this.items = this.originalItemsArray;
                return;
            }
            let newTable = [];
            this.originalItemsArray.forEach(element => {
                if(element.StorageLocation.toUpperCase().startsWith(this.sortByLocationText.toUpperCase())) {
                    newTable.push(element);
                }
            });
           this.items = newTable;      
        },
        async getItemList() {
            try {
                const response = await axios.get('http://localhost:5171/TMARequest/GetRequests');
                if (response.data) {
                    return response.data;
                } else {
                    throw new Error("Adding item failed");
                }
            } catch (error) {
                if(error.response && error.response.status == 404) {
                    alert("Cannot find any item in database");
                }
                else {
                alert('Error' + error);
                }
            }
        },
        mapToRequestResponse(response) {
            const items = []
            response.forEach(itemData => {
                const item = new TMARequest(
                itemData.requestID,
                itemData.employeeName,
                itemData.comment,
                itemData.status,

            );
            items.push(item);
            });
            return items;
        },
        async removeItem() {
            if(this.selectedIndex == null) {
                alert("You have to select item");
                return;
            }
            const id = Number(this.originalItemsArray[Number(this.selectedIndex)].ItemID);
            axios.post('http://localhost:5171/Item/DeleteItem',id,{headers: {
                'Content-Type': 'application/json'
                }
            }).then(response =>{
                if(response.status == 200) {
                    alert("succesfully deleted item from database");
                    this.originalItemsArray.splice(this.selectedIndex,1);
                    this.items = this.originalItemsArray;
                    console.log(this.items);
                }
            }).catch(error =>{
                alert("Error: ",error);
            })
            
        },
        updateItem() {
            if(this.selectedIndex == null) {
                alert("You have to select item");
                return;
            }
            this.$emit('updateItemRequest',this.originalItemsArray[this.selectedIndex]);
        },
    },
    created() {
        this.getItemList().then(response => {
            console.log(response);
            this.items = this.mapToRequestResponse(response);
            this.originalItemsArray = this.items;
            console.log(this.items);
            }).catch(error => {
                console.error("Error", error);
            });
        }
    }
</script>
<style scoped>
.container {
    margin-top: 3%;
    display: flex;
    height: auto;
    max-height: 80vh;
    width: 90vh;
    align-items: center;
}
.headers {
    width: 90vw;
    height: 10%;
    display: flex;
    border: solid rgb(105, 105, 105) 4px;
    border-bottom: none;
}
.headers > button {
    width: calc(100% / 4); 
    height: 100%;
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    text-decoration: none;
    text-shadow: 0 0 10px #1201ff; 
    font-size: large;
    font-weight: bolder; 
    overflow: scroll;
    cursor: pointer;
}

.headers > button::-webkit-scrollbar {
    display: none;
}
.headerButtonDiv{ 
    display: flex;
    justify-content: end;
    width: 90%;
}
.searchInput {
    display: flex;
    align-items: center;
    justify-content: left;
    width: 90vw;
    padding-bottom: 20px;
}
.searchInput > input {
    height: 100%;
    margin-right: 1%;
}

.itemsDiv {
    display: flex;
    flex-direction: column;
    width: 90vw;
    height: 100%;
    overflow-y: auto;
    border: solid rgb(105, 105, 105) 4px;
    justify-content: start;
    align-items: start;
}

.itemsDiv::-webkit-scrollbar {
    display: none;
}
button:hover {
    opacity: 0.5;
}
.buttonsDiv{
    display:flex;
    align-items: center;
    justify-content: center;
    width: 90%;
    height: 10%;
    padding-top:1%;
}
.buttonsDiv > button {
    height: 100%;
    width: 100%;
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    text-decoration: none;
    text-shadow: 0px 0px 10px #3ee8ff; /* Dodanie efektu cienia */
    font-size: large;
    font-weight: bolder; 
    cursor: pointer;
}
@media screen and (max-width: 720px) {
    .headers > button { 
        font-size: 11px;
        font-weight:lighter;
        text-align: center;
        overflow: scroll;   
    }
}
</style>