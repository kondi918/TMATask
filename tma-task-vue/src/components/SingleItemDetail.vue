<template>
    <div class="singleWordContainer">
        <div class="singleWord"> <h3> {{ item.itemName }} </h3>  </div>
        <div class="singleWord"> <h3> {{ item.quantity }} </h3>  </div>
        <div class="singleWord"> <h3> {{ item.unitOfMeasurement }} </h3>  </div>
        <div class="singleWord"> <h3> {{ item.priceWithoutVAT }} </h3>  </div>
    </div>  
</template>

<script>


export default {
    props:{
        item: Object
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
    width: calc(100% / 4); 
    align-items: start;
    justify-content: center;
    height: 100%;
    background-color: rgba(255, 255, 255, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    border-top:none;
    overflow: auto;
}
.singleWord > button {
    color: rgb(255, 255, 255);
    background-color: rgb(0, 0, 0);
    text-decoration: none;
    text-shadow: 0px 0px 10px #3ee8ff;
    font-size: large;
    font-weight: bolder; 
    cursor: pointer;
    height: 100%;
    width: 100%;
}
.singleWord > button:hover {
    background-color: rgb(156, 156, 156);
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
    height: 20%;
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