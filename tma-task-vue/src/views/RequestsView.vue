<template>    
<div class="loginSite" v-if="myUser && myUser.OPPermission">
    <div class="container">
        <div class="headerDiv">
                <div class="homeNavDiv">
                    <router-link to="/"> Home </router-link>
                </div>
                <div class="otherNavDiv"> 
                    <router-link to="/warehouse">Item List</router-link>
                    <router-link to="/requests" v-if="myUser.OPPermission">Request List</router-link>
                    <router-link to="/about" v-if="myUser.ADMPermission">Admin Panel</router-link>
                </div>
                <div class="userInfoDiv">
                    Logged User: <br>
                    {{ myUser.username }} 
                    {{ myUser.role }}
                </div>
        </div>
        <div class="bodyDiv" v-if="isListShowing">
           <RequestList @goToDetails="handleDetailsShowing"> </RequestList>
        </div>
        <div class="bodyDiv" v-if="!isListShowing">
            <RequestDetails @goBack="handleGoBack" :rows="requestRows" :requestID="requestID"> </RequestDetails>
         </div>
    </div>
    <div class="backgroundBlack"> </div>
</div>
  </template>
  <script>
import User from '@/Data/User';
import RequestList from '@/components/RequestList.vue';
import RequestDetails from '@/components/RequestDetails.vue';
import axios from 'axios'
import RequestRowsResponse from '@/Data/RequestRowsResponse';

export default {
  props: ['user'],
  components: {
    RequestList,
    RequestDetails
  },
  data() {
    return {
        myUser: User,
        isListShowing: true,
        requestRows : Array,
        requestID : Number
    }
  },
  created() {
    this.myUser = JSON.parse(localStorage.getItem('user'));
    if(this.myUser == null || this.myUser.role == null) {
        this.$router.push({ name: 'login'});
    }
  },
  methods: {
    async handleDetailsShowing(objectID) {
        this.requestID = objectID;
        this.requestRows = await this.getRequestRows(objectID)
        this.isListShowing = false;
    },
    handleGoBack() {
        this.isListShowing = true;
    },
    mapToRequestRowsResponse(response) {
            const items = []
            response.forEach(itemData => {
                const item = new RequestRowsResponse(
                itemData.itemID,
                itemData.itemName,
                itemData.priceWithoutVAT,
                itemData.quantity,
                itemData.unitOfMeasurement
            );
            items.push(item);
            });
            return items;
        },
    async getRequestRows(objectID){
        try {
        const response = await axios.get(`http://localhost:5171/TMARequest/GetRequestRows?itemID=${objectID}`);
        if (response.data) {
            return this.mapToRequestRowsResponse(response.data);
        } else {
            alert("Empty response");
            return []; 
        }
    } catch (error) {
        console.log(error);
        alert('Error:', error);
        return []; 
    }
    }
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