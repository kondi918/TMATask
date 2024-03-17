<template>
    <div class="container">
        <div class="searchInput">
           <input type="text" placeholder="search by name">
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
        </div>
        <div class="itemsDiv">
        <WarehouseSingleItem v-for="(number,index) in numbers" 
        :key="index" 
        :number="number"
        :selected="index===selectedIndex" 
        @click="selectItem(index)"/>
        </div>
        <div class="buttonsDiv">
            <button @click="addItem()"> Add </button>
            <button> Update </button>
            <button> Remove </button>
        </div>
    </div>
</template>

<script>
import WarehouseSingleItem from './WarehouseSingleItem.vue';
export default {
    data() {
        return {
            numbers: [1,2,3,4,5],
            selectedIndex: null
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
            console.log(`Sort by ${column}`);
        },
        addItem() {
            this.$emit('addObject');
        }
    }
};
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
    width: calc(100% / 9); 
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
.searchInput {
    display: flex;
    align-items: center;
    justify-content: left;
    width: 90vw;
    padding-bottom: 20px;
}
.searchInput > input {
    height: 100%;
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