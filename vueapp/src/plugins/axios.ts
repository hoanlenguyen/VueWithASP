import axios from 'axios'
import type {App} from 'vue'
import type {CreateAxiosDefaults } from 'axios'
interface AxiosOptions {
    baseUrl?: string
    token?: string
}
const baseApiUrl = import.meta.env.VITE_Api_Url;

export default {
    install: (app: App, options: AxiosOptions) => {
        app.config.globalProperties.$axios = axios.create({
            baseURL: baseApiUrl,
            headers: {
                Authorization: options.token ? `Bearer ${options.token}` : '',
                'Content-type':'application/json'
            }
        } as CreateAxiosDefaults)
    }
}