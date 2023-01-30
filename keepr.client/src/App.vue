<template>
  <header class="mb-4 m-2 p-2">
    <Banner />
  </header>
  <main>
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


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
