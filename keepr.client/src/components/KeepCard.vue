<template>
  <div class="card elevation-5 hover-card">

    <!-- <router-link :to"{name:'Vault', params: {id: vault.id}}"></router-link> -->
    <img :src="keep?.img" alt="http://thiscatdoesnotexist.com" class="img-fluid hover-shadow">
    {{ keep?.name }}
  </div>


</template>

<script>

export default {

  setup(props) {

    return {
    
    };

  }
}
</script>

<style>

</style>