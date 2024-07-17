<template>
  
</template>

<script setup lang="ts">
import GroupItem from '@/components/GroupItem.vue';
import orderApi from '@/api/order.api';
import { ref } from 'vue';
import ButtonComponent from '@/components/ButtonComponent.vue';

const selected = ref ({
    id: undefined,
    customerId: undefined,
    amount: undefined,
});


</script>
