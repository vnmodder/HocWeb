<template>
  <header class="bg-dark py-5">
    <div class="container px-4 px-lg-5 my-5">
      <div class="text-center text-white">
        <h1 class="display-4 fw-bolder">Shop Account Học Web</h1>
        <p class="lead fw-normal text-white-50 mb-0">
          Demo shop account với .NET Core API và Vue Js 3
        </p>
      </div>
    </div>
  </header>
  <section class="pt-5">
    <h2 class="fw-bolder text-center">Danh mục</h2>
    <div class="container px-4 px-lg-5 mt-5">
      <div
        class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center"
      >
        <div v-for="(item, index) in categories" :key="index" class="col mb-5">
          <a class="nav-link" :href="'/product?categoryId=' + item.id">
            <div class="card h-100">
              <img class="card-img-top" :src=" rootFile +item.image" alt="..." />
              <div class="card-body p-4">
                <div class="text-center">
                  <h5 class="fw-bolder">{{ item.nameVN }}</h5>
                </div>
              </div>
            </div>
          </a>
        </div>
      </div>
    </div>
  </section>
  <section >
    <h2 class="fw-bolder mb-4 text-center">Các tài khoản hot</h2>
    <div class="container px-4 px-lg-5 mt-2">
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
            <a :href="'/product-detail?id=' + item.id">
              <img class="card-img-top" :src="item.image" alt="..." />
            </a>
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
                <a
                  class="btn btn-outline-dark mt-auto"
                  :href="'/product-detail?id=' + item.id"
                  >Xem</a
                >
              </div>
              <div class="text-center">
                <button
                  class="btn btn-outline-dark mt-auto ms-2"
                  @click="addToCart(item)"
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
import homeApi from "@/api/home.api";
import { ref } from "vue";
import { useCartStore } from "@/stores/cart";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";
import {rootFile} from '@/components/contants'

const authStore = userStore();
const { user } = storeToRefs(authStore);

const products = ref<any>([]);
const categories = ref<any>([]);

const addToCart = (item: any) => {
  if (!user.value) {
    alert("Hãy đăng nhập để thực hiện chức năng này");
    return;
  }

  const product = {
    id: item.id,
    name: item.name,
    unitPrice: item.unitPrice,
    quantity: 1,
  };
  useCartStore().addToCart(product);
};

const fetchData = async () => {
  const response = await homeApi.getAllProduct();
  if (response && response.data.result.isSuccess) {
    products.value = response.data.result.data;
  } else {
    products.value = [];
    alert(response?.data.result.message ?? "Lỗi");
  }

  const response2 = await homeApi.getAllCategory();
  if (response2 && response2.data.result.isSuccess) {
    categories.value = response2.data.result.data;
  } else {
    categories.value = [];
    alert(response2?.data.result.message ?? "Lỗi");
  }
};

fetchData();
</script>
