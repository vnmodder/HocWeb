import baseApi from './base.api';

export default {
    getAllCategory: async () => {
      return await baseApi.get('Category/get-all');
    },
    getCategory: async (id:string|number) => {
      return await baseApi.get('Category/get-by-id?id='+id);
    },
    AddNewCategory: async (data: FormData) => {
      return await baseApi.postForm('Category/add',data);
    },
    getImage: async (img: string) => {
      return await baseApi.getPublicFile(img);
    },
};