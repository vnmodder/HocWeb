<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import PageNavbar from "@/components/PageNavbar.vue";
import PageFooter from "@/components/PageFooter.vue";
</script>

<template>
  <div class="col-md-10 mx-auto">
  <!-- Header -->
  <PageNavbar />
  <!-- end header  -->

  <!-- content -->
  <RouterView />
  <!-- end content -->

  <!-- footer-->

  <PageFooter />

  <!-- end footer -->
  </div>

</template>
