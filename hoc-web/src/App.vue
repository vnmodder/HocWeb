<script setup lang="ts">
import {  RouterView } from "vue-router";
import PageNavbar from "@/components/PageNavbar.vue";
import PageFooter from "@/components/PageFooter.vue";

import '@/assets/bootstrap.min.css'
</script>

<template>
  <PageNavbar />
  <div class="hocweb-app">
    <RouterView />
  </div>
  <PageFooter />
</template>

<style scoped>

.hocweb-app{
  min-height: calc(100vh - 11.2rem);
}

</style>
