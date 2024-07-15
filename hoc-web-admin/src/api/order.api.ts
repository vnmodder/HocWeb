import baseApi from "./base.api";

export default {
    getallorder: async () => {
        return await baseApi.get('Order/get-all');
    },
    getOrderbyId: async (id:string|number)=>{
        return await baseApi.get('Order/get-by-id?id='+id);
    },
    getbyUserId: async (id:string|number) => { 
        return await baseApi.get('Order/get-by-user-id?id='+id);
      },
    updateOrder: async (data: FormData) => {
        return await baseApi.put('Order/update', data);
    },
    deleteOrder: async (id:string|number) => {
        return await baseApi.delete('Order/delete?id='+id);
    },
    addOrder: async (model: any) => {
        return await baseApi.post('Order/add', model);
    }
    
}
