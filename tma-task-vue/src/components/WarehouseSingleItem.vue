<template>
        <div class="singleWord"> <h3> 2</h3>  </div>
        <div class="singleWord"> <h3> fruits</h3>  </div>
        <div class="singleWord"> <h3> box</h3>  </div>
        <div class="singleWord"> <h3> 40</h3>  </div>
        <div class="singleWord"> <h3> 100</h3>  </div>
        <div class="singleWord"> <h3> needed</h3>  </div>
        <div class="singleWord"> <h3> Warsaw</h3>  </div>
        <div class="singleWord"> <h3> Greg Kaminsky</h3>  </div>
        <div class="singleWord"> <img src="../assets/apple.jpg"> </div>
        
</template>
<style scoped>
.singleWord {
    display: flex;
    width: calc(100% / 9); 
    align-items: center;
    text-align: center;
    justify-content: center;
    height: 10%;
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
</style>