<template>
    <section class="pt-1">
      <div class="container px-2 px-lg-5 mt-1" v-if="items">
        <h2 class="fw-bolder mb-4 text-center">Giỏ hàng của bạn</h2>
        <div class="row gx-4 gx-lg-5 align-items-center">
          <table class="table table-striped">
            <thead>
              <tr class="text-center">
                <th scope="col">#</th>
                <th scope="col">Chọn</th>
                <th scope="col">Tên sản phẩm</th>
                <th scope="col">Đơn giá</th>
                <th scope="col">Số lượng</th>
                <th scope="col">Thành tiền</th>
                <th scope="col">Tùy chọn</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in items" :key="index">
                <th scope="row" class="text-center">{{ index + 1 }}</th>
                <td class="text-center">
                  <input type="checkbox" v-model="item.checked" @change="useCart.updateCheck()" />
                </td>
                <td>{{ item.name }}</td>
                <td class="text-end">
                  {{ new Intl.NumberFormat("vi-vn").format(item.unitPrice) }}đ
                </td>
                <td class="text-center">{{ item.quantity }}</td>
                <td class="text-end">
                  {{
                    new Intl.NumberFormat("vi-vn").format(
                      item.quantity * item.unitPrice
                    )
                  }}đ
                </td>
                <td class="text-center">
                  <i class="fas fa-trash text-danger" @click="delClick(item)">Xoá</i>
                </td>
              </tr>
            </tbody>
          </table>
  
          <h5 class="mb-4 text-end">
            Tổng tiền toàn giỏ hàng:
            <span class="fw-bold">
              {{ new Intl.NumberFormat("vi-vn").format(useCart.cartTotal()) }}đ
            </span>
          </h5>
        </div>
        <div class="d-flex justify-content-center">
          <a class="btn btn-dark fw-bolder" href="/checkout">Tiến hành thanh toán</a>
        </div>
        <h5 class="mb-4 text-center mt-2">
          Tổng tiền đang chọn:
          <span class="fw-bold">
            {{ new Intl.NumberFormat("vi-vn").format(useCart.chosedTotal()) }}đ
          </span>
        </h5>
      </div>
      <div class="container px-2 px-lg-5 mt-1" v-else>
        <h2 class="fw-bolder mb-4 text-center">Chưa có sản phẩm nào</h2>
      </div>
    </section>
  </template>
  
  <script setup lang="ts">
  import { storeToRefs } from "pinia";
  import { useCartStore } from "../stores/cart";
  
  const useCart = useCartStore();
  const { items } = storeToRefs(useCart);
  
  const delClick = (item: any) => {
    const result = window.confirm("Xác nhận xóa " + item.name);
    if (result) {
      useCart.removeCartItem(item.id);
    }
  };
  </script>
  