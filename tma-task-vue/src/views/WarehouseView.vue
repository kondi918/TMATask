<template>    
<div class="loginSite" v-if="myUser">
    <div class="container">
        <div class="headerDiv">
                <div class="homeNavDiv">
                    <router-link to="/"> Home </router-link>
                </div>
                <div class="otherNavDiv"> 
                    <router-link to="/warehouse">Item List</router-link>
                    <router-link to="/requests" v-if="myUser.OPPermission">Request List</router-link>
                    <router-link to="/admin" v-if="myUser.ADMPermission">Admin Panel</router-link>
                </div>
                <div class="userInfoDiv">
                    Logged User: <br>
                    {{ myUser.username }} 
                    {{ myUser.role }}
                </div>
        </div>
        <div class="bodyDiv" v-if="!showAddingPanel && !showOrderPanel">
            <WarehouseItemList @addObject="handleAddingObject" @updateItemRequest="handleUpdateRequest" @orderRequest="handleOrderRequest" @clearRequest="handleClearRequest()" :myUser="myUser"></WarehouseItemList>
        </div>
        <div class="bodyDiv" v-if="showAddingPanel">
            <AddingItemPanel @returnToList="handleReturnToList" :whatPanel="whatPanel" :givenItem="givenItem"> </AddingItemPanel>
        </div>
        <div class="bodyDiv" v-if="showOrderPanel">
            <OrderPanel @returnToList="handleReturnToList"  @addToOrderList="handleAddingOrderToRequest" @requestSubmit="handleRequestSubmit" :givenItem="givenOrder" :employerName="myUser.username"> </OrderPanel>
        </div>
    </div>
    <div class="backgroundBlack"> </div>
</div>
  </template>
  <script>
import WarehouseItemList from '@/components/WarehouseItemList.vue';
import AddingItemPanel from '@/components/AddingItemPanel.vue';
import OrderPanel from '@/components/OrderPanel.vue';
import axios from 'axios';
import User from '@/Data/User';

export default {
  components: {
    WarehouseItemList,
    AddingItemPanel,
    OrderPanel
  },
  data() {
    return {
        myUser: User,
        showAddingPanel: false,
        showOrderPanel: false,
        whatPanel: 'adding',
        givenItem: Object,
        givenOrder: Object,
        orderRequestList: []
    }
  },
  created() {
    this.myUser = JSON.parse(localStorage.getItem('user'));
    if(this.myUser == null || this.myUser.role == null) {
        this.$router.push({ name: 'login'});
    }
  },
  methods: {
    handleAddingObject() {
        this.showAddingPanel = true;
        this.whatPanel = 'adding';
    },
    handleReturnToList() {
        this.showAddingPanel = false;
        this.showOrderPanel = false;
    },
    handleUpdateRequest(item) {
        this.givenItem = item
        this.whatPanel = 'updating'
        this.showAddingPanel = true;
    },
    handleOrderRequest(givenItem) {
        this.givenOrder = givenItem;
        this.showOrderPanel = true;
    },
    handleAddingOrderToRequest(item) {
        this.showOrderPanel = false;
        this.orderRequestList.push(item)
        alert("Request updated");
    },
    handleRequestSubmit(item) {
        this.showOrderPanel = false;
        this.orderRequestList.push(item)
        this.sendOrdersRequest();
    },
    handleClearRequest() {
        this.orderRequestList = [];
        alert("Request cleared");
    },
    async sendOrdersRequest() {
            axios.post('http://localhost:5171/TMARequest/PostOrderRequest',this.orderRequestList,{headers:{
                'Content-Type': 'application/json'}
                })
            .then(response => {
                if(response)
                {
                    console.log(response.data);
                    alert("Request created");
                    this.orderRequestList = [];
                }
                else{
                    alert("Creating request failed. Try again");
                }
            })
            .catch(error => {
                console.log(error);
                alert('Error:', error);
            });
        },
  }
}
</script>

<style scoped>
.loginSite {
  position: fixed;
  background-image: url("../assets/WarehouseBackground.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  height: 100vh;
  width: 100vw;
  left: 0;
  top: 0;
  object-fit: cover;
  overflow: hidden;
  padding: 0;
  margin: 0;
}
body,html {
  padding: 0;
  margin: 0;
}
.backgroundBlack {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  z-index: -1;
  object-fit: cover; 
  background-color: rgba(0, 0, 0, 0.822);
}
a {
    color:white;
    padding: 10%;
    font-family: 'Roboto', sans-serif; 
    letter-spacing: 2px;
    font-size: 16px; 
    font-weight: bolder; 
    line-height: 1.6; 
    color: #ffffff;
    text-decoration: none;
    background-image: linear-gradient(90deg, #b3e0ff, #ffffff); /* Gradient kolorów dla tekstu */
    -webkit-background-clip: text; /* Ustawienie tła jako clip dla tekstu */
    background-clip: text; /* Ustawienie tła jako clip dla tekstu */
    text-shadow: 0 0 10px #b3e0ff; /* Dodanie efektu cienia */  
}
a:hover {
    opacity: 0.5;
}
.container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 100vw;
    height: 100vh;
}
.headerDiv {
    display: flex;
    flex-direction: row;
    width: 100vw;
    height: 10%;
    align-items: center;
    justify-content: center;
    background-color: rgba(29, 25, 25, 0.753);
}
.homeNavDiv {
    display: flex;
    width: 20vw;
    height: 100%;
    align-items: center;
    justify-content: center;
}
.homeNavDiv > a {
    font-family: 'Arial', sans-serif;
    color: transparent;
    background-image: linear-gradient(90deg, #b3e0ff, #ffffff); /* Gradient kolorów dla tekstu */
    -webkit-background-clip: text; /* Ustawienie tła jako clip dla tekstu */
    background-clip: text; /* Ustawienie tła jako clip dla tekstu */
    text-shadow: 0 0 10px #3ee8ff; /* Dodanie efektu cienia */
    font-size: 200%;
}
.otherNavDiv {
    display: flex;
    width: 60vw;
    height: 100%;
    justify-content: end;
    align-items: center;
}
.bodyDiv {
    width: 100vw;
    height: 90%;
}
.userInfoDiv {
    display: flex;
    align-items: end;
    justify-content: end;
    padding-right: 1%;
    width: 20vw;
    color:white;
}
@media screen and (max-width: 720px) {
    a {
        font-size: 3vw;
        padding: 2%;
    }
    .homeNavDiv > a {
        font-size: 5vw;
    }
    .userInfoDiv {
        font-size: 3vw;
    }
    
}
</style>