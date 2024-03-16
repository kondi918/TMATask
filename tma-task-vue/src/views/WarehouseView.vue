<template>    
<div class="loginSite">
    <nav>
        <router-link to="/"> Home </router-link> |
        <router-link to="/about">About</router-link>
    </nav>
    <div class="backgroundBlack"> </div>
</div>
  </template>
  <script>
import User from '@/Data/User';

export default {
  props: ['user'],
  data() {
    return {
        myUser: User,
        name: ''
    }
  },
  created() {
    this.myUser = JSON.parse(localStorage.getItem('user'));
    if(this.myUser == null) {
        this.$router.push({ name: 'login'});

    }
    this.name = this.myUser.username;
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
nav {
    color: white;
}
& > router-link {
    color:White;
}
a {
    color:white;
}
</style>