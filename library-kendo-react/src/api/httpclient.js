import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:5293/api", // perhatikan: http, port 5293
});

export function setAuthToken(token) {
    if (token) {
        http.defaults.headers.common["Authorization"] = `Bearer token`;
    } else {
        delete http.defaults.headers.common["Authorization"];
    }
}

export default http;
