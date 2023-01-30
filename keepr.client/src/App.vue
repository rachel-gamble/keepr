<template>
  <header class="app-bg">
    <Banner />
  </header>
  <main class="app-bg">
    <router-view />
  </main>
  <footer class="">
    Made with ✨💀🌙 by Sautrah
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import Banner from './components/Banner.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Banner }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  background-color: #FEF6F0;
}

.app-bg {
  background-color: #FEF6F0;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
