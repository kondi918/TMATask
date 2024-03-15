<template>
  <div class="backgroundBlack"> </div>
  <div class="vidDiv">
  <video class="bgVideo" autoplay loop muted>
    <source src="../assets/HomeSiteBackground.mp4" type="video/mp4" />   
 </video>
</div>
<HomeSite></HomeSite>
</template>

<script>
import HomeSite from '@/components/HomeSite.vue';

// @ is an alias to /src
export default {
  components: {
    HomeSite
  }
};
</script>

<style scoped>
.bgVideo {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  z-index: -2; 
  object-fit: cover; 
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
