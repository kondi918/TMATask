<template>
<div class="container">
    <h2 v-if="isAdding">Add User Form</h2>
    <h2 v-if="!isAdding">Update User Form</h2>
    <div class="formContainer">
        <form id="addItemForm" @submit.prevent="handleSubmit">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" required v-model="username">  
        <label for="password">Password:</label>
        <input type="password" id="password" name="password" required v-model="password">
        <label for="confirmPassword">Confirm Password:</label>
        <input type="password" id="confirmPassword" name="confirmPassword" required v-model="passwordConfirm">                            
        <label for="role">Role:</label>
        <select id="role" name="role" required v-model="role">
        <option value="" selected disabled hidden>Select Role</option>
        <option value="EMP">EMP</option>
        <option value="CO">CO</option>
        <option value="ADM">ADM</option>
        </select>
        <button v-if=isAdding type="submit">Add User</button>
        <button v-if=!isAdding type="submit">Update User</button>
        <button @click="cancelAdding"> Cancel </button>
        </form>
    </div>
</div>
</template>
<script>
import axios from 'axios';
import AddUserRequest from '@/Data/AddUserRequest';
import UpdateUserRequest from '@/Data/UpdateUserRequest';

export default {
    props: {
        givenItem : Object,
        whatPanel: String
    },
    data() {
        return {
        username: '',
        password: '',
        passwordConfirm: '',
        role: '',
        isAdding : true
        };
    },
    methods: {
        cancelAdding() {
            this.$emit("returnToList");
        },
        isPasswordCorrect() {
            if(this.password == this.passwordConfirm) {
                return true
            }
            return false;
        },
        async handleSubmit(event) {
        event.preventDefault();
        if(!this.isPasswordCorrect()) {
            alert('Passwords are not same')
            return;
        }
        if(this.isAdding) {
        var addingRequest = new AddUserRequest(this.username,this.password,this.role)
        await this.sendAddRequest(addingRequest);
        }
        else {
            var updatingRequest = new UpdateUserRequest(this.givenItem, this.username, this.password, this.role)
            this.sendUpdateRequest(updatingRequest)
        }
        },
        async sendAddRequest(requestBody) {
            axios.post('http://localhost:5171/User/AddUser',requestBody)
            .then(response => {
                if(response)
                {
                    alert("Succesfully added to database");
                    this.cancelAdding();
                }
                else{
                    alert("Adding user failed");
                }
            })
            .catch(error => {
                alert("Error: " + error.response.data);
            });
        },
        async sendUpdateRequest(requestBody) {
            axios.post('http://localhost:5171/User/UpdateUser',requestBody)
            .then(response => {
                if(response)
                {
                    alert("Succesfully updated user in database");
                    this.cancelAdding();
                }
                else{
                    alert("Updating user failed");
                }
            })
            .catch(error => {
                alert('Error:' + error.response.data);
            });
        },
  },
  created() {
    if(this.whatPanel == "adding") {
        this.isAdding = true
    }
    else {
        this.isAdding = false;
    }
  }
}
</script>
<style scoped>

.formContainer {
    display: flex;
    width: 60%;
    height: 90%;
    justify-content: center;
    align-items: center;
}
form {
    height: 100%;
    width: 100%;
    padding: 0px;
    margin: 0px;
}
h2 {
    color:white;
}
label {
    color:white;
}
input {
    width: 100%;
    height: calc(100%/20);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
select {
    width: 100%;
    height: calc(100%/20);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
textarea {
    width: 100%;
    height: calc(100%/20);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
button {
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    text-decoration: none;
    text-shadow: 0px 0px 10px #3ee8ff;
    font-size: large;
    font-weight: bolder; 
    cursor: pointer;
    margin-right: 2%;
}
button:hover {
    opacity:0.5;
}
@media screen and (max-width: 720px) {
    .formContainer {
        width: 80%;
    }
    form > * {
        margin-bottom: 3%;
    }   
}
</style>