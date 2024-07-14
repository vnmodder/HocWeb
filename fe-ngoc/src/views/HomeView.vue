<template>
    <div class="container mt-6 mb-6">
      <div class="row">
        <div class="col-lg-4 col-md-6 mb-4" v-for="item in products" :key="item.id">
          <div class="card h-100">
            <img :src="item.image" class="card-img-top" :alt="`Product ${item.name}`">
            <div class="card-body">
              <h5 class="card-title">{{ item.name }}</h5>
              <p class="card-text">{{ item.description }}</p>
            </div>
            <div class="card-footer d-flex justify-content-between">
              <a :href="'/Product_detail?Id=' + item.id" class="btn btn-primary">View</a>
              <button class="btn btn-secondary" @click="AddToCart(item)">Add to Cart</button>
            </div>
          </div>
        </div>
      </div>
      <br><br>
    </div>
</template>
  

<script setup lang='ts'>
import { ref } from 'vue';
import homeApi from '@/api/home.api';
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
// console.log(useCartStore().getAllProducts())
const fetchData = async () => {
    const response = await homeApi.getAllProduct();
    // console.log(response);
    if(response && response.data.result.isSuccess){
        products.value = response.data.result.data; 
    }
    else{
        alert(response?.data.result.message ?? "Lỗi");
    }
};
fetchData();

</script>