// // api-client.ts
// import { AccommodationApi, AddressApi, Configuration, TestApi } from './open-api/src/api';

// export function createApiClient(basePath = process.env.REACT_APP_API_BASE_URL) {
//     const config = new Configuration({
//         basePath: basePath
//     });

//     return {
//         accommodation: new AccommodationApi(config),
//         address: new AddressApi(config),
//         test: new TestApi(config)
//         // altri client API...
//     };
// }

import { AccommodationApi, AddressApi, Configuration, TestApi } from './open-api/src/api';

// URL hardcoded dell'API - da usare quando process.env non è disponibile
const API_BASE_URL = "https://proxy-production-197b.up.railway.app";

export function createApiClient(basePath = API_BASE_URL) {
    const config = new Configuration({
        basePath: basePath
    });

    return {
        accommodation: new AccommodationApi(config),
        address: new AddressApi(config),
        test: new TestApi(config)
        // altri client API...
    };
}