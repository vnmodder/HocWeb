import baseApi from './base.api';

export default {
    getAllProduct: async () => {
      return await baseApi.get('Product/get-all');
    },
};