<template>

<div class="container mt-5">
        <h2 class="mb-4">Giỏ hàng của bạn</h2>
        <table  class="table table-striped">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Chọn</th>
                    <th scope="col">Tên sản phẩm</th>
                    <th scope="col">Đơn giá</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody id="cart-items" v-for="(item,index) in items">
                <tr>
                    <td>{{ index+1 }}</td>
                    <td>
                        <input type="checkbox" v-model="item.checked" @change="useCart.updateCheck()">
                    </td>
                    <td>
                        {{ item.name }}
                    </td>
                    <td>
                        {{ item.unitPrice }}
                    </td>
                    <td>
                        <i class="fas fa-trash text-danger" @click="delclick(item)"></i>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="d-flex justify-content-between align-items-center mt-4">
            <h4 id="cart-total">Tổng tiền: {{ useCart.chosedTotal() }}</h4>
            <a href="">
                <button  @click="payclick" class="btn btn-primary">Thanh toán</button>
            </a>
            
        </div>
    </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useCartStore } from '@/stores/cart';


const useCart = useCartStore();
// useCart.loadCart();
const {items} = storeToRefs(useCart);

const delclick = (item : any) => {
    const result = window.confirm("delete: " + item.name + "?");
    if(result){
        useCart.removeCartItem(item.id);
    }
};
const payclick = () => {
    const hasSelectedItems = items.value.some(item => item.checked);
    if (!hasSelectedItems) {
        alert("Vui lòng chọn sản phẩm để thanh toán.");
        return;
    }
    const result = window.confirm("xac nhan thanh toan?");
    if(result){
        useCart.clearSelectedCart();
    }
} 

</script>