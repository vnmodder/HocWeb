<template>
<div class="container mt-5">
    <div class="row">
        <div class="col-md-6">
            <img :src="product.image" class="img-fluid rounded" alt="Product Image">
        </div>
        <div class="col-md-6">
            <h1>{{ product?.name }}</h1>
            <p>Description: {{ product?.description }}</p>
            <p>Quantity: <strong>{{ product.quantity }}</strong></p>
            <p>Price: <strong>{{ product.unitPrice }}</strong></p>
            <button class="btn btn-secondary" @click="AddToCart(product)">Add to Cart</button>
          </div>
    </div>
</div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRoute } from "vue-router";
import productDApi from "@/api/product-detail.api";
import { userStore } from '@/stores/auth';
import { storeToRefs } from 'pinia';
import { useCartStore } from '@/stores/cart';

const authStore = userStore();
const {user} = storeToRefs(authStore);

const products = ref<any>([]);

const AddToCart = (item : any) =>{
    if(!user.value){
        alert("pls login to have right to manipulate!");
        return
    }
    const product = {
        id: item.id,
        name: item.name,
        unitPrice: item.unitPrice,
        quantity: 1,
    }
    useCartStore().addToCart(product);
}

const route = useRoute();
const product = ref<any>();

const fetchData = async () => {
  if(route.query.Id){
    const response = await productDApi.getProductById(route.query.Id?.toString());
    if(response && response.data.result.isSuccess){
      product.value = response.data.result.data;
    }
  }
  else{
    
  }
};


fetchData();
// useCartStore().loadCart();
</script>