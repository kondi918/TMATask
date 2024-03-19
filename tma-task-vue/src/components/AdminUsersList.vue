<template>
    <div class="container">
        <div class="headers">
            <div> ID </div>
            <div> Username </div>
            <div> Role </div>
        </div>
        <div class="itemsDiv">
        <SingleUser v-for="(item,index) in items" 
        :key="index"
        :item="item"
        :selected="index===selectedIndex" 
        @click="selectItem(index)"/>
        </div>
        <div class="buttonsDiv">
            <button @click="addItem()"> Add </button>
            <button @click="updateItem"> Update </button>
            <button @click="removeItem()"> Remove </button>
        </div>
    </div>
</template>

<script>
import SingleUser from './SingleUser.vue';
import axios from 'axios';

export default {
    props: {
        myUser : Object
    },
    data() {
        return {
            selectedIndex: null,
            items : [],
        }
    },
    components: {
        SingleUser
    },
    methods: {
        selectItem(index) {
            this.selectedIndex = index;
        },
        async getItemList() {
            try {
                const response = await axios.get('http://localhost:5171/User/GetUsers');
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
                alert("Error: " + error.response.data);
            })
            
        },
        updateItem() {
            if(this.selectedIndex == null) {
                alert("You have to select item");
                return;
            }
            this.$emit('updateItemRequest',this.originalItemsArray[this.selectedIndex]);
        },
    },
    created() {
        this.getItemList().then(response => {
            this.items = response;
            }).catch(error => {
                console.error("Error: " + error.response.data);
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
    width: 90%;
    height: 10%;
    display: flex;
    border: solid rgb(105, 105, 105) 4px;
}
.headers > div {
    width: calc(100% / 3); 
    height: 100%;
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    border-bottom: none;
    text-decoration: none;
    text-shadow: 0 0 10px #1201ff; 
    font-size: large;
    font-weight: bolder; 
    overflow: scroll;
    box-sizing: border-box;
    display: flex;
    justify-content: center;
    align-items: center;
}
.headers > h3 {
    width: calc(100% / 10); 
    height: 100%;
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
}
.headers > div::-webkit-scrollbar {
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
    text-shadow: 0px 0px 10px #3ee8ff;
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