import axios from "axios";
import ServerURL from "./server_url";

const axiosInstance = axios.create({baseURL: ServerURL});

export default axiosInstance;