<template>

<div class="container mt-5">
        <h2 class="mb-4">Giỏ hàng của bạn</h2>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Chọn</th>
                    <th scope="col">Tên sản phẩm</th>
                    <th scope="col">Đơn giá</th>
                    <th scope="col">Số lượng</th>
                    <th scope="col">Hành động</th>
                </tr>
            </thead>
            <tbody id="cart-items" v-for="(item,index) in items">
                <tr>
                    <td>{{ index+1 }}</td>
                    <td>
                        <input type="checkbox" v-model="item.checked" @change="useCart.updateCheck()">
                    </td>
                    <td>
                        {{ item.name }} 1
                    </td>
                    <td>
                        {{ item.unitPrice }}
                    </td>
                    <td>
                        {{ item.quantity }}
                    </td>
                    <td></td>
                </tr>
            </tbody>
        </table>
        <div class="d-flex justify-content-between align-items-center mt-4">
            <h4 id="cart-total">Tổng tiền: 0</h4>
            <button class="btn btn-primary">Thanh toán</button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useCartStore } from '@/stores/cart';
// import { useCounterStore } from '@/stores/counter';

const useCart = useCartStore();
const {items} = storeToRefs(useCart);

// const counter = useCounterStore();

</script>