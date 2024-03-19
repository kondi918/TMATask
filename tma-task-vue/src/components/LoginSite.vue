<template>
  <div class="container">
   <div class="headerDiv"> 
    <h1> Login Form </h1>
   </div>
    <div class="loginForm">
        <form @submit.prevent="login">
          <fieldset>
            <label> Username </label>
            <input type="text" v-model="username" required placeholder="your username">
            <label> Password </label>
            <input type="password" v-model="password" required placeholder="your password">
            <input type="submit" class="loginButton" value="Login">
          </fieldset>
        </form>
    </div>
  </div>
  </template>
<script>
import User from '@/Data/User';
import axios from 'axios';
export default {
  data() {
    return {
      username: '',
      password: ''
    };
  },
  methods: {
    RedirectToWarehouse() {
      this.$router.push({ name:'warehouse'});
    },
    GetAlertString() {
      var alertString = `Succesfully Added init Users to Database
        Login Details:
        1) Employeer
        Username:employee
        Password:emp123
        2) Coordinator
        Username:coordinator
        Password:cor123
        3) Administrator
        Username:administrator
        Password:adm123
        
        More details in README file`
        return alertString;
    },
    async login() {
      axios.post('http://localhost:5171/User/PostUser', {
      username : this.username,
      password : this.password,
  })
    .then( response => {
      if(response.status == 201)
      {
        alert(this.GetAlertString())
      }
      else if(response.status == 200) {
        console.log(response)
        const userData = response.data
        let user = new User(userData.username, userData.role);
        localStorage.setItem('user', JSON.stringify(user));
        this.RedirectToWarehouse();
      }
      else {
        console.log(response);
      }
    })
    .catch(error => {
      if(error.response != null){
        alert(error + "\n" + error.response.data);
      }
      else {
        alert(error + " There might be a problem with connection to API");
      }
    });
  }

}};
</script>
<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100vw;
  height: 100vh;
}
.loginForm {
  height: 50%;
  width: 50%;
}
.loginForm input {
  width: 100%;
  height: 30px;
  margin-bottom: 10px;
}
.loginForm label {
  color:white;
  font-size: larger;
}
.headerDiv {
  height: 30%;
  width: 50%;
}
h1{
  color:white;
}
.loginButton {
  margin-top: 30px;
  font-size: larger;
  font-weight: bolder;
}
fieldset {
  background-color: rgba(0, 0, 0, 0.445);
}

@media screen and (max-width: 768px) {
  .loginButton {
    width: 100%;
  } 
}
</style>