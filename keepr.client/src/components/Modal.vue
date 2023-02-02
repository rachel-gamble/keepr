<template>
  <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content app-bg">
        <div class="modal-header app-bg">
          <slot name="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">{{ modalTitle }}</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </slot>
        </div>
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    modalTitle: {
      type: String,
      required: false,
      default: 'Close'
    },
  },
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>

</style>