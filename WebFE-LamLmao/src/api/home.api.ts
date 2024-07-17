import baseApi from './base.api';

export default {
    getAllCategory: async () => {
      return await baseApi.get('Category/get-all');
    },

    getAllProduct: async () => {
      return await baseApi.get('Product/get-all');
    }
};

