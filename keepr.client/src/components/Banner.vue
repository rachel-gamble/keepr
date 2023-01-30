<template>
  <section class="row app-bg m-3">
    <section class="col-md-12 nav-section">
      <div class="left-section d-flex">

        <!--SECTION HOME BUTTON-->

        <router-link class="" :to="{ name: 'Home' }">
          <button class="btn home-btn">
            <p class="home-btn p-2">Home</p>
          </button>
        </router-link>

        <!--SECTION CREATE BUTTON-->
        <div class="dropdown show justify-content-center">
          <a class="btn btn dropdown-toggle create-button m-2 pb-2" href="#" role="button" id="dropdownMenuLink"
            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Create
          </a>

          <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
            <a class="dropdown-item" href="#">Keep</a>
            <a class="dropdown-item" href="#">Vault</a>
          </div>
        </div>

      </div>
      <!--SECTION Mid-Right // Login and Logo-->
      <div class="">
        <h1 class="keepr-logo p-1 mx-3 px-3">the<br>keepr<br>co.</h1>
      </div>
      <div class="">
        <Login />
      </div>
      <!--END-->
    </section>
  </section>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style>
section {
  background-color: #FEF6F0;
}

.nav-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

left-section {
  display: flex;
}

.keepr-logo {
  /* position: absolute;
  width: 47px;
  height: 60px; */
  left: calc(50% - 47px/2 - 7.76px);
  top: calc(50% - 60px/2 - 0.63px);

  font-family: 'Marko One', serif;
  font-style: normal;
  font-weight: 400;
  font-size: 15.2491px;
  line-height: 20px;
  display: flex;
  align-items: center;

  color: #2D2D2D;

  box-sizing: border-box;
  border: 1.66787px solid #2D2D2D;
  border-radius: 4.76534px;
}

.home-btn {
  background-color: #E9D8D6;
  border-radius: 15px;
  font-family: 'Oxygen', sans-serif;
  font-weight: 700;
}

.create-button {
  font-family: 'Oxygen', sans-serif;
  background-color: #FEF6F0;
  font-weight: 700;
}
</style>