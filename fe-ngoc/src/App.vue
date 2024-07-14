<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import PageNavBar from '@/components/PageNavBar.vue'
import PageFooter from '@/components/PageFooter.vue'
import { useCartStore } from './stores/cart';

useCartStore().loadCart();

// test 
</script>

<template>
  <header>
    <PageNavBar/>
  </header>
  <RouterView/>
  <footer>
    <PageFooter/>
  </footer>
</template>


