<template>
    <div class="singleWordContainer" :class="{selected: selected}">
        <div class="singleWord"> <h3> 2</h3>  </div>
        <div class="singleWord"> <h3> fruits</h3>  </div>
        <div class="singleWord"> <h3> box</h3>  </div>
        <div class="singleWord"> <h3> 40</h3>  </div>
        <div class="singleWord"> <h3> 100</h3>  </div>
        <div class="singleWord"> <h3> needed</h3>  </div>
        <div class="singleWord"> <h3> Warsaw</h3>  </div>
        <div class="singleWord"> <h3> Greg Kaminsky</h3>  </div>
        <div class="singleWord"> <img src="../assets/apple.jpg"> </div>
    </div>  
</template>

<script>
export default {
    props: ['selected']
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
    align-items: center;
    text-align: center;
    justify-content: center;
    height: 100%;
    background-color: rgba(255, 255, 255, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    border-top:none;
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