<template>
<div class="upDiv">
 <h1> TMA Warehouse Solution </h1>
</div>
<div class="downDiv">
<router-link to="login"> 
<button class='glowing-btn'><span class='glowing-txt'>L<span class='faulty-letter'>O</span>GIN</span> </button>
</router-link>
</div>
</template>

<script>
export default {
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1 {
  font-size: 4vw;
  text-shadow: 1px 10px 10px #ffffff; 
  letter-spacing: 2px;
  line-height: 1; 
  text-transform: uppercase; 
  display: inline-block; 
  background: linear-gradient(to right, #00c3ff 50%, #008cff 50%); 
  -webkit-background-clip: text; 
  -webkit-text-fill-color: transparent;
  animation: glow-text-shadow 10s infinite linear;
}
.upDiv {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 40vh;
}
.downDiv {
  display: flex;
  height: 50vh;
  justify-content: center;
  align-items: center;
}
.glowing-btn {
  position: relative;
  color: white;
  cursor: pointer;
  padding: 0.35em 1em;
  border: 0.15em solid rgb(71, 81, 133);
  border-radius: 0.45em;
  background: rgb(0, 0, 0);
  perspective: 2em;
  font-family: "Raleway", sans-serif;
  font-size: 2em;
  font-weight: 900;
  letter-spacing: 1em;
  animation: glowing-border 1.5s infinite alternate;
}

@keyframes glowing-border {
  0% {
    border-color: rgb(71, 81, 133);
  }
  100% {
    border-color: rgb(0, 255, 255); 
  }
}
@keyframes glow-text-shadow {
  0%, 100% {
    text-shadow: 1px 10px 10px #000000; 
  }
  25% {
    text-shadow: 5px 5px 10px #000000;
  }
  50% {
    text-shadow: 10px 5px 5px #000000;
  }
  75% {
    text-shadow: 1px -10px 10px #000000;
  }
}

.glowing-txt {
  float: left;
  margin-right: -0.8em;
  -webkit-text-shadow: 0 0 0.125em hsl(0 0% 100% / 0.3),
    0 0 0.45em blue;
  -moz-text-shadow: 0 0 0.125em hsl(0 0% 100% / 0.3),
    0 0 0.45em blue;
  text-shadow: 0 0 0.125em hsl(0 0% 100% / 0.3), 0 0 0.45em blue;
  animation: text-flicker 3s linear infinite;
}

.faulty-letter {
  opacity: 0.5;
  animation: faulty-flicker 2s linear infinite;
}


@keyframes text-flicker {
  0% {
    opacity: 0.1;
  }

  2% {
    opacity: 1;
  }

  8% {
    opacity: 0.1;
  }

  9% {
    opacity: 1;
  }

  12% {
    opacity: 0.1;
  }
  20% {
    opacity: 1;
  }
  25% {
    opacity: 0.3;
  }
  30% {
    opacity: 1;
  }

  70% {
    opacity: 0.7;
  }
  72% {
    opacity: 0.2;
  }

  77% {
    opacity: 0.9;
  }
  100% {
    opacity: 0.9;
  }
}

@media screen and (max-width: 768px) {
  h1 {
    font-size: 6vw;
  }
  .glowing-btn {
    font-size: 1.5em; 
    padding: 0.35em 1em;
    font-weight: 900;
  }
}
</style>
