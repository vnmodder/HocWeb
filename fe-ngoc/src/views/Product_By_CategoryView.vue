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
                    <a href="#" class="btn btn-primary">View</a>
                    <button class="btn btn-secondary" @click="AddToCart(item)">Add to Cart</button>
                </div>
            </div>
        </div>
    </div>
    <br><br>
</div>

</template>

<script setup lang='ts'>
import { useRoute } from 'vue-router';
import { ref } from 'vue';
import productApi from '@/api/product.api';
import { userStore } from '@/stores/auth';
import { storeToRefs } from 'pinia';
import { useCartStore } from '@/stores/cart';

const route = useRoute();
const products = ref<any>([]);
const categoryName = ref<any>();

const authStore = userStore();
const {user} = storeToRefs(authStore);

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

const fetchData = async () => {
  if(route.query.categoryId){
    const cateres = await productApi.getCategoryById(
        route.query.categoryId?.toString()
    );
    // console.log(cateres);
    if(cateres && cateres.data.result.isSuccess){
        categoryName.value = cateres.data.result.data?.nameVN;
    }
  }
  const response = await productApi.getAllProduct();
//   console.log(response)
  if(response && response.data.result.isSuccess){
    if (route.query.categoryId) {
      products.value = response.data.result.data?.filter(
        (x: any) => x.categoryId == route.query.categoryId
      );
    }
    else{
        products.value = response.data.result.data;
    }
  }
  else{
    products.value = [];
    alert(response.data.result.message);
  }
  console.log(products.value);
};
fetchData();
// useCartStore().loadCart();
</script>