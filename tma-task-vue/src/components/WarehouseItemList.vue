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
        <WarehouseSingleItem></WarehouseSingleItem>
        </div>
    </div>
</template>

<script>
import WarehouseSingleItem from './WarehouseSingleItem.vue';
export default {

    components: {
        WarehouseSingleItem
    },
    methods: {
        sortBy(column) {
            console.log(`Sort by ${column}`);
        }
    }
};
</script>
<style scoped>
.container {
    margin-top: 5%;
    display: flex;
    height: auto;
    max-height: 70vh;
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
    font-size: larger;
    font-weight: bolder; 
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
@media screen and (max-width: 720px) {
    .headers > button { 
        font-size: 11px;
        font-weight:lighter;
        text-align: center;
        overflow: scroll;
    }
}
</style>