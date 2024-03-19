<template>
    <div class="detailsContainer">
        <h3> Request Details </h3>
        <div class="headers">
            <button>ItemName </button>
            <button>Quantity</button>
            <button> UnitOfMeasurement </button>
            <button>PriceWithoutVAT</button>
        </div>
        <div class="itemsDiv">
        <SingleItemDetail v-for="(item,index) in rows"
        :key="index"
        :item="item"
        />
        </div>
        <div class="buttonsDiv">
            <button @click="goBack()"> Cancel </button>
            <button @click="confirmRequest()"> Confirm the Request </button>
            <button @click="rejectRequest()"> Reject the Request </button>
        </div>
    </div>
</template>

<script>
import SingleItemDetail from './SingleItemDetail.vue';
import RejectRequest from '@/Data/RejectRequest';
import axios from 'axios';

export default {
    props: {
        rows : Array,
        requestID : Number
    },
    data() {
        return {
            previousColumn : 'ID',
            direction : 1,
        }
    },
    components: {
        SingleItemDetail,
    },
    methods: {
        goBack() {
            this.$emit('goBack');
        },
        async rejectRequest() {
            var comment = prompt("Why rejecting");
            var requestBody = new RejectRequest(this.requestID,'rejected',comment)
            await this.rejectRequestHandler(requestBody)
            this.goBack();
        },
        async rejectRequestHandler(requestBody) {
            console.log(requestBody);
            await axios.post('http://localhost:5171/TMARequest/RejectRequest',requestBody,{headers: {
                'Content-Type': 'application/json'
                }
            }).then(response =>{
                if(response.status == 200) {
                    alert("Request Rejected");
                }
            }).catch(error =>{
                alert("Error: " + error.response.data);
            })
        },
        async confirmRequest() {
            await axios.post('http://localhost:5171/TMARequest/ConfirmRequest',this.requestID, {headers: {
                'Content-Type': 'application/json'
            }}).then(response => {
                if(response.status == 200) {
                    alert("Request Confirmed");
                    this.goBack();
                }
            }).catch(error => {
                alert("Error: " + error.response.data);
            })
        }
    },
    }
</script>
<style scoped>
h3 {
    color:white;
}
.detailsContainer {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    justify-content: center;
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
    height: 60%;
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