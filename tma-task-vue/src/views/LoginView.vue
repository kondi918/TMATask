<template>
  <div class="loginSite">
    <div class="backgroundBlack"> </div>
    <LoginSite> </LoginSite>
  </div>
</template>

<script>
import LoginSite from '@/components/LoginSite.vue';
export default{
  components: {
    LoginSite
  }
};
</script>
<style scoped>
.loginSite {
  position: fixed;
  background-image: url("../assets/LoginBackground.jpg");
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

</style>