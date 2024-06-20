<template>
    <section>
            <div
            class="container px-1 px-lg-1 mt-1"
            v-if="orderDs"
          >
            <div class="row gx-1 gx-lg-1 align-items-center">
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
                    v-for="(item, index) in orderDs"
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
                      total
                    )
                  }}đ
                </span>
              </h5>
            </div>
          </div>
    </section>
</template>

<script setup lang="ts">
import { ref } from "vue";
import orderDApi from "@/api/order-detail.api";

const orderDs = ref<any>([]);
const total = ref(0)
const props = defineProps<{
    orderId?: number
}>();

const fetchData = async () => {
    const response = await orderDApi.getOrderDetail(props.orderId ?? 0);
    if (response && response.data.result.isSuccess) {
        orderDs.value = response.data.result.data
    } else {
        orderDs.value = [];
        alert(response.data.result.message);
    }
    total.value = 0
    orderDs.value.forEach((item: any) => {
        total.value += item.quantity * item.unitPrice
    });
};

fetchData()
</script>