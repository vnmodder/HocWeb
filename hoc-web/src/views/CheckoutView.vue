<template>
  <section>
    <div class="container px-4 px-lg-5 mt-5">
      <div class="row gx-4 gx-lg-5">
        <div class="col-md-8">
          <div
            class="container px-2 px-lg-5 mt-1"
            v-if="items.filter((x) => x.checked)"
          >
            <h2 class="fw-bolder mb-4 text-center">Đơn hàng</h2>
            <div class="row gx-4 gx-lg-5 align-items-center">
              <table class="table table-striped">
                <thead>
                  <tr class="text-center">
                    <th scope="col">#</th>
                    <th scope="col">Tên sản phẩm</th>
                    <th scope="col">Đơn giá</th>
                    <th scope="col">Số lượng</th>
                    <th scope="col">Thành tiền</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(item, index) in items.filter((x) => x.checked)"
                    :key="index"
                  >
                    <th scope="row" class="text-center">{{ index + 1 }}</th>
                    <td>{{ item.name }}</td>
                    <td class="text-end">
                      {{
                        new Intl.NumberFormat("vi-vn").format(item.unitPrice)
                      }}đ
                    </td>
                    <td class="text-center">{{ item.quantity }}</td>
                    <td class="text-end">
                      {{
                        new Intl.NumberFormat("vi-vn").format(
                          item.quantity * item.unitPrice
                        )
                      }}đ
                    </td>
                  </tr>
                </tbody>
              </table>

              <h5 class="mb-4 text-end">
                Tổng tiền:
                <span class="fw-bold">
                  {{
                    new Intl.NumberFormat("vi-vn").format(
                      useCart.chosedTotal()
                    )
                  }}đ
                </span>
              </h5>
            </div>
          </div>
        </div>
        <div class="col-md-4">
          <h2 class="fw-bolder mb-4 text-center">Thông tin</h2>

          <div class="form-group mt-2">
            <label class="label" for="name">Tên bạn</label>
            <input
              type="text"
              class="form-control mt-2"
              :value="user?.username"
              placeholder="Tên"
              disabled
            />
          </div>

          <div class="form-group mt-2">
            <label class="label" for="email">Email</label>
            <input
              type="email"
              class="form-control mt-2"
              :value="user?.email"
              placeholder="Email"
              disabled
            />
          </div>

          <div class="form-group mt-2">
            <label class="label" for="name">Địa chỉ</label>
            <input
              type="text"
              class="form-control mt-2"
              v-model="address"
              placeholder="Địa chỉ"
            />
          </div>

          <div class="form-group mt-2">
            <label class="label" for="#">Ghi chú</label>
            <textarea
              v-model="description"
              class="form-control mt-2"
              cols="30"
              rows="4"
              placeholder="Ghi chú"
            ></textarea>
          </div>
          <div class="d-flex justify-content-center mt-3">
            <button class="btn btn-dark fw-bolder" @click="onCheckout" >Đặt hàng</button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import {ref} from "vue"
import { storeToRefs } from "pinia";
import { useCartStore } from "../stores/cart";
import { userStore } from "../stores/auth";
import checkoutApi from "@/api/checkout.api";

const useCart = useCartStore();
const { items } = storeToRefs(useCart);

const authStore = userStore();
const { user } = storeToRefs(authStore);

const address = ref()
const description = ref()

const onCheckout = async ()=>{
  const selectItem = items.value.filter(x=>x.checked)
  if(!selectItem){
    alert("Hiện không có sản phẩm nào được chọn")
    return
  }

  const payload = {
    customerId: user.value?.userId,
    address: address.value,
    description: description.value,
    amount: useCart.chosedTotal(),
    orderDetails: selectItem.map(x=> {
        return {
            productId: x.id,
            unitPrice: x.unitPrice,
            quantity: x.quantity,
        }})
  }

  const response = await checkoutApi.createNewOrder(payload);
  if (response && response.data.result.isSuccess) {
    alert('Đã đặt hàng thành công!');
    useCart.clearSelectedCart();
  } else {
    alert(response?.data.result.message??'Lỗi');
  }
}
</script>
