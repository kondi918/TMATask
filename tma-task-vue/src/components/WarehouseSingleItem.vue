<template>
    <div class="singleWordContainer" :class="{selected: selected}">
        <div class="singleWord"> <h3>{{ item.ItemID }} </h3>  </div>
        <div class="singleWord"> <h3> {{item.ItemGroup}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.UnitOfMeasurement}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.Quantity}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.PriceWithoutVAT}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.Status}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.StorageLocation}}</h3>  </div>
        <div class="singleWord"> <h3> {{item.ContactPerson}}</h3>  </div>
        <div class="singleWord"> <img :src="'data:image/png;base64,'+item.Photo" alt="test"> </div>
    </div>  
</template>

<script>
import ItemResponse from '@/Data/ItemResponse';

export default {
    props:{
        selected : Boolean,
        item: ItemResponse
    },
};
</script>


<style scoped>
.singleWordContainer {
    display: flex;
    width: 100%;
    height: 10%;
    cursor: pointer;
}
.singleWordContainer:hover {
    opacity: 0.8;
}
.singleWord {
    display: flex;
    width: calc(100% / 9); 
    align-items: start;
    justify-content: center;
    height: 100%;
    background-color: rgba(255, 255, 255, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    border-top:none;
    overflow: auto;
}
.singleWord::-webkit-scrollbar {
    display: none;
}
img {
    height: 100%;
    width: 100%;
}
h3 {
    font-size: normal;
    font-weight: lighter;
    color:white;
    text-align: center;
    justify-content: center;
    height: 100%;
}
@media screen and (max-width: 720px) {
    h3 { 
        font-size: 11px;
        font-weight:lighter;
        text-align: center;
        overflow: scroll;
    }
}
.selected {
    background-color: rgb(11, 87, 27);
}
</style>