<template>
    <div class="container">
        <div class="searchInput">
           <input type="text" placeholder="search by id" @input="sortByNumber" v-model="sortByNumberText">
           <input type="text" placeholder="search by employee" @input="sortByName()" v-model="sortByNameText">
        </div>
        <div class="headers">
            <button @click="sortBy('ID')">ID </button>
            <button @click="sortBy('EmployeeName')">Employee Name</button>
            <button> Comment </button>
            <button @click="sortBy('Status')">Status</button>
        </div>
        <div class="itemsDiv"> 
            <SingleRequest v-for="(item,index) in items" 
            :key="index"
            :item="item"
            @click="selectItem(index)"/>
        </div>
    </div>
</template>

<script>
import TMARequest from '@/Data/TMARequest';
import axios from 'axios';
import SingleRequest from './SingleRequest.vue';

export default {
    data() {
        return {
            items : [],
            previousColumn : 'ID',
            direction : 1,
            originalItemsArray : [],
            sortByNameText : '',
            sortByNumberText: '',
        }
    },
    components: {
        SingleRequest,
    },
    methods: {
        selectItem(index) {
            if(this.items[index].Status != 'done') {
            this.$emit('goToDetails',this.items[index].RequestID);
            }
        },
        sortBy(column) {
            if(this.previousColumn == column) {
                this.direction *= -1;
            }
            this.previousColumn = column;
            if(this.items != null && this.items.length > 1) {
                switch (column) {
                    case 'ID':
                        this.items.sort((a, b) => (a.RequestID - b.RequestID) * this.direction);
                        break;
                    case 'EmployeeName':
                        this.items.sort((a, b) => ((a.EmployeeName || '').localeCompare(b.EmployeeName || '')) * this.direction);
                        break;
                    case 'Status':
                        this.items.sort((a, b) => ((a.Status || '').localeCompare(b.Status || '')) * this.direction);
                        break;
                    default:
                        this.items.sort((a, b) => a.ItemID - b.ItemID);
                        break;
                    }
                }
        },
        sortByNumber() {
            if(this.sortByNumberText == '') {
                this.items = this.originalItemsArray;
                return;
            }
            let newTable = [];
            this.originalItemsArray.forEach(element => {
                if(element.RequestID.toString().startsWith(this.sortByNumberText)) {
                    newTable.push(element);
                }
            });
           this.items = newTable;      
        },
        sortByName() {
            if(this.sortByNameText == '') {
                this.items = this.originalItemsArray;
                return;
            }
            let newTable = [];
            this.originalItemsArray.forEach(element => {
                if(element.EmployeeName.toUpperCase().startsWith(this.sortByNameText.toUpperCase())) {
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
    },
    created() {
        this.getItemList().then(response => {
            this.items = this.mapToRequestResponse(response);
            this.originalItemsArray = this.items;
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