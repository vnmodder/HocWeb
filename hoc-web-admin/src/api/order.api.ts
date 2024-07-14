import baseApi from "./base.api";

export default {
    getallorder: async () => {
        return await baseApi.get('Order/get-all');
    },
    getOrderbyId: async ()=>{
        return await baseApi.get('Order/get-by-id');
    },
    getbyUserId: async () => { 
        return await baseApi.get(`Order/get-by-user-id`);
      },
    updateOrder: async (data: FormData) => {
        return await baseApi.put(`Order/update`, data);
    },
    deleteOrder: async () => {
        return await baseApi.delete(`Order/delete`);
    },
    addOrder: async (model: any) => {
        return await baseApi.post('Order/add', model);
    }
    
}
