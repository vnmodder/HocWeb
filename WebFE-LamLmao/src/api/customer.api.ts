import baseApi from "./base.api";
export default {
    login: async (model: LoginModel): Promise<any | undefined> => {
        const response = await baseApi.postAuthenticate('Authenticate/login', model);
        if (response.status == 200) {
            return response.data;
        }
    },
    customerregister: async (password: string, name: string, email: string) => {
        return await baseApi.postAuthenticate('Customer/add', {
            password,
            name,
            email,
        });
    }

}
export interface LoginModel {
    userName: string;
    password: string;
}

export interface LoginResponseModel{
    name:string,
    blance:Number,
    customerId:Number,
    email: string,
    token: string,
    expiration: string,
}