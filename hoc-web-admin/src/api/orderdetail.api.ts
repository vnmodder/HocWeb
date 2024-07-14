import baseApi from "./base.api";
export default {   
    getallorderdetail: async () => {
        return await baseApi.get('OrderDetail/get-all');
    },
    getOrderDetailbyId: async (orderId: number| string) => {
        return await baseApi.get('OrderDetail/get-by-oder-id?oderId=' +orderId);
      },
    updateOrderDetail: async (data: FormData) => {
        return await baseApi.put('OrderDetail/update', data);
    },
    deleteOrderDetail: async () => {
        return await baseApi.delete('OrderDetail/delete');
    },
    addOrderDetail: async (model: any) => {
        return await baseApi.post('OrderDetail/add', model);
    }
}