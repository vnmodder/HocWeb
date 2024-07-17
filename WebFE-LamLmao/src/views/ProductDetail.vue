<template>
    <section class="py-1">
      <div class="container px-4 px-lg-5 my-5">
        <div class="row gx-4 gx-lg-5 align-items-center">
          <div class="col-md-6">
            <img
              class="card-img-top mb-5 mb-md-0"
              :src="product?.image"
            />
          </div>
          <div class="col-md-6">
            <h1 class="display-5 fw-bolder">{{ product?.name }}</h1>
            <div class="fs-5 mb-5">
              <span
                >{{
                  new Intl.NumberFormat("vi-vn").format(product.unitPrice)
                }}đ</span
              >
            </div>
            <p class="lead">{{ product?.description }}</p>
            <div class="d-flex">
              <input
                class="form-control text-center me-3"
                type="num"
                v-model="quantity"
                style="max-width: 3rem"
              />
              <button
                class="btn btn-outline-dark flex-shrink-0"

                type="button"
              >
                <i class="bi-cart-fill me-1"></i>
                Thêm vào giỏ hàng
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section class="bg-light pt-2">
      <div class="container px-2 px-lg-5 mt-1">
        <h2 class="fw-bolder mb-4">Tài khoản cùng loại</h2>
        <div
          class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center"
        >
          <div v-for="(item, index) in products" :key="index" class="col mb-5">
            <div class="card h-100">
              <!-- Sale badge-->
              <div
                class="badge bg-dark text-white position-absolute"
                style="top: 0.5rem; right: 0.5rem"
              >
                Sale
              </div>
              <!-- Product image-->
              <a :href="'/product-detail?id=' + item.id">
                <img class="card-img-top" :src="item.image" alt="..." />
              </a>
              <!-- Product details-->
              <div class="card-body p-4">
                <div class="text-center">
                  <!-- Product name-->
                  <h5 class="fw-bolder">{{ item.name }}</h5>
                  <!-- Product reviews-->
  
                  <!-- Product price-->
                  <span
                    >{{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ
                  </span>
                </div>
              </div>
              <div
                class="card-footer p-4 pt-0 border-top-0 bg-transparent d-flex justify-content-center"
              >
                <div class="text-center">
                  <div class="text-center">
                    <a
                      class="btn btn-outline-dark mt-auto"
                      :href="'/product-detail?id=' + item.id"
                      >Xem</a
                    >
                  </div>
                </div>
                <div class="text-center">
                  <button
                    class="btn btn-outline-dark mt-auto ms-2"
                    
                  >
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
  import { ref } from "vue";
  import { useRoute } from "vue-router";
  import productDApi from "@/api/product-detail.api";
  import { useCartStore } from "@/stores/cart";
  import { storeToRefs } from "pinia";
  import { userStore } from "../stores/auth";
  
  const authStore = userStore();
  const { user } = storeToRefs(authStore);
  const route = useRoute();
  const product = ref<any>();
  const products = ref<any>([]);
  const quantity = ref<number>(1);
  
  const fetchData = async () => {
  if (route.query.id) {
    const res = await productDApi.getProductById(route.query.id?.toString());
    if (res && res.data.result.isSuccess) {
      product.value = res.data.result.data;
      const response = await productDApi.getAllProduct();
      if (response && response.data.result.isSuccess) {
        products.value = response.data.result.data?.filter(
          (x: any) => x.categoryId == product.value?.categoryId
        );
      } else {
        products.value = [];
      }
    } else {
      alert(res.data.result.message);
    }
  }
};
  
    fetchData();
  </script>
  