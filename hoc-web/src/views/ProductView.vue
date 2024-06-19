<template>
  <section>
    <div class="container px-4 px-lg-5 mt-5">
      <h1> {{ categoryName??'Tất cả sản phẩm' }}</h1>
      <div
        class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center"
      >
        <div v-for="(item, index) in products" :key="index" class="col mb-5">
          <div class="card h-100">
            <!-- Sale badge-->
            <div
              v-if="index % 2 == 1"
              class="badge bg-dark text-white position-absolute"
              style="top: 0.5rem; right: 0.5rem"
            >
              Sale
            </div>
            <!-- Product image-->
            <img class="card-img-top" :src="item.image" alt="..." />
            <!-- Product details-->
            <div class="card-body p-4">
              <div class="text-center">
                <!-- Product name-->
                <h5 class="fw-bolder">{{ item.name }}</h5>
                <!-- Product reviews-->
                <!-- <div class="d-flex justify-content-center small text-warning mb-2">
                                         <div class="bi-star-fill"></div>
                                         <div class="bi-star-fill"></div>
                                         <div class="bi-star-fill"></div>
                                         <div class="bi-star-fill"></div>
                                         <div class="bi-star-fill"></div>
                                     </div> -->
                <!-- Product price-->
                <!-- <span class="text-muted text-decoration-line-through">$20.00</span> -->
                <span
                  >{{
                    new Intl.NumberFormat("vi-vn").format(item.unitPrice)
                  }}đ</span
                >
              </div>
            </div>
            <!-- Product actions-->
            <div
              class="card-footer p-4 pt-0 border-top-0 bg-transparent d-flex justify-content-center"
            >
              <div class="text-center">
                <div class="text-center"><a class="btn btn-outline-dark mt-auto" :href="'/product-detail?id=' +item.id " >Xem</a></div>
              </div>
              <div class="text-center">
                <button class="btn btn-outline-dark mt-auto ms-2">
                  Thêm vào giỏ
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { useRoute } from "vue-router";
import productApi from "@/api/product.api";
import { ref } from "vue";

const route = useRoute();
const products = ref<any>([]);
const categoryName = ref<any>();

const fetchData = async () => {
    console.log(route.query)
    if(route.query.categoryId){
        const cateRes = await productApi.getCategoryById(route.query.categoryId?.toString());
        if (cateRes && cateRes.data.result.isSuccess) {
            categoryName.value= cateRes.data.result.data?.nameVN
        }
    }

  const response = await productApi.getAllProduct();
  if (response && response.data.result.isSuccess) {
    if (route.query.categoryId) {
      products.value = response.data.result.data?.filter(
        (x: any) => x.categoryId == route.query.categoryId
      );
    } else {
      products.value = response.data.result.data;
    }
  } else {
    products.value = [];
    alert(response.data.result.message);
  }
};

fetchData();
</script>
