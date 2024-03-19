<template>
    <div class="container">
        <div class="searchInput">
           <input type="text" placeholder="search by name" @input="sortByName(value)" v-model="sortByNameText">
           <input type="text" placeholder="search by location" @input="sortByLocation()" v-model="sortByLocationText">
           <div class="headerButtonDiv"><button @click="clearRequestEmit"> Clear Order Request </button> </div>
        </div>
        <div class="headers">
            <button @click="sortBy('ID')">ID </button>
            <button @click="sortBy('ItemGroup')">Item Group</button>
            <button @click="sortBy('UnitOfMeasurement')">Unit Of Measurement</button>
            <button @click="sortBy('Quantity')">Quantity</button>
            <button @click="sortBy('PriceWithoutVAT')">Price Without VAT</button>
            <button @click="sortBy('Status')">Status</button>
            <button @click="sortBy('StorageLocation')">Storage Location</button>
            <button @click="sortBy('ContactPerson')">Contact Person</button>
            <button> Photo </button>
            <h3> Order </h3>
        </div>
        <div class="itemsDiv">
        <WarehouseSingleItem v-for="(item,index) in items" 
        :key="index"
        :item="item"
        :selected="index===selectedIndex" 
        @click="selectItem(index)" @orderEmit="orderRequest"/>
        </div>
        <div class="buttonsDiv" v-if="myUser.OPPermission">
            <button @click="addItem()"> Add </button>
            <button @click="updateItem"> Update </button>
            <button @click="removeItem()"> Remove </button>
        </div>
    </div>
</template>

<script>
import ItemResponse from '@/Data/ItemResponse';
import WarehouseSingleItem from './WarehouseSingleItem.vue';
import axios from 'axios';
import User from '@/Data/User';

export default {
    props: {
        myUser:User
    },
    data() {
        return {
            selectedIndex: null,
            items : [],
            previousColumn : 'ID',
            direction : 1,
            originalItemsArray : [],
            sortByNameText : '',
            sortByLocationText: ''
        }
    },
    components: {
        WarehouseSingleItem
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
                    case 'ItemGroup':
                        this.items.sort((a, b) => ((a.ItemGroup || '').localeCompare(b.ItemGroup || '')) * this.direction);
                        break;
                    case 'UnitOfMeasurement':
                        this.items.sort((a, b) => ((a.UnitOfMeasurement || '').localeCompare(b.UnitOfMeasurement || '')) * this.direction);
                        break;
                    case 'Quantity':
                        this.items.sort((a, b) => ((a.Quantity || 0) - (b.Quantity || 0)) * this.direction);
                        break;
                    case 'PriceWithoutVAT':
                        this.items.sort((a, b) => ((a.PriceWithoutVAT || 0) - (b.PriceWithoutVAT || 0)) * this.direction);
                        break;
                    case 'Status':
                        this.items.sort((a, b) => ((a.Status || '').localeCompare(b.Status || '')) * this.direction);
                        break;
                    case 'StorageLocation':
                        this.items.sort((a, b) => ((a.StorageLocation || '').localeCompare(b.StorageLocation || ''))*this.direction);
                        break;
                    case 'ContactPerson':
                        this.items.sort((a, b) => ((a.ContactPerson || '').localeCompare(b.ContactPerson || '')) * this.direction);
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

        addItem() {
            this.$emit('addObject');
        },
        async getItemList() {
            try {
                const response = await axios.get('http://localhost:5171/Item/GetItems');
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
        mapToItemResponse(response) {
            const items = []
            response.forEach(itemData => {
                const item = new ItemResponse(
                itemData.itemID,
                itemData.itemGroup,
                itemData.unitOfMeasurement,
                itemData.quantity,
                itemData.priceWithoutVAT,
                itemData.status,
                itemData.storageLocation,
                itemData.contactPerson,
                itemData.photo
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
        prepareItemNames() {
            const itemArray = []
            this.originalItemsArray.forEach(element => {
                const elementType = element.ItemGroup;
                const elementUnitOfMeasurement = element.UnitOfMeasurement;
                const quantity = element.Quantity
                const singleItem = {
                    ItemGroup : elementType,
                    UnitOfMeasurement : elementUnitOfMeasurement,
                    Quantity : quantity
                }
                if(!itemArray.includes(singleItem)) {
                    itemArray.push(singleItem);
                }
            });
            return itemArray
        },
        orderRequest(item) {
            this.$emit('orderRequest', item)
        },
        clearRequestEmit() {
            this.$emit('clearRequest')
        },
    },
    created() {
        this.getItemList().then(response => {
            this.items = this.mapToItemResponse(response);
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
    width: calc(100% / 10); 
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
.headers > h3 {
    width: calc(100% / 10); 
    height: 100%;
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
}
.headers > button::-webkit-scrollbar {
    display: none;
}
.headerButtonDiv{ 
    display: flex;
    justify-content: end;
    width: 90%;
}
.headerButtonDiv > button {
    color: rgb(255, 255, 255);
    background-color: rgb(0, 0, 0);
    text-decoration: none;
    text-shadow: 0px 0px 10px #3ee8ff; /* Dodanie efektu cienia */
    font-size: large;
    font-weight: bolder; 
    cursor: pointer;
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