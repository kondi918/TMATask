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
import axios from 'axios';
export default {
  data() {
    return {
      username: '',
      password: ''
    };
  },
  methods: {
    async login() {
      const response = await axios.get('http://localhost:5171/WeatherForecast');
      console.log(response);
    }
  }
};
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