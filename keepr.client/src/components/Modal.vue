<template>
<div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
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