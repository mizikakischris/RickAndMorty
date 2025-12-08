# RickAndMorty

## Business Logic: Data Retrieval Rules

This project follows a caching strategy to minimize external API calls and improve performance.  
Data come from the **External API**, **Database**, or **Cache**, depending on the condition.

---

### 📋 Data Source Rules

| Condition                         | Source of Data          |
| --------------------------------- | ------------------------ |
| **First request / cache empty**   | API → Database → Cache  |
| **Within 5 minutes (cache valid)** | Cache                   |
| **After 5 minutes (cache expired)** | API → Database → Cache  |
| **Manual addition**               | Database → Update Cache |

---

### 🔎 Explanation

#### First request
When there is no cached data, the service fetches data from the external **Rick & Morty API**, stores it in the **Database**, and then populates the **Cache**.

#### Valid cache (≤ 5 minutes)
When the cache is still fresh, data is returned **directly from cache** without querying the database or calling the external API.

#### Expired cache (> 5 minutes)
If the cache is expired, the service fetches new data from the **External API**, updates the **Database**, and refreshes the **Cache**.

#### Manual addition
When a user manually adds a character, the **Database** is updated and the **Cache** is immediately synchronized to include the new entry.

###  API Endpoints
| Method | Endpoint        | Description              |
|--------|------------------|--------------------------|
| GET    | /api/characters  | Returns a list of characters |

###  Custom Response Headers

The API may include custom headers to provide metadata.

| Header Name     | Description                                  |
|-----------------|----------------------------------------------|
| from-database   | `true` if data came from database, otherwise `false` |


## 🌐 Web App Integration

This solution includes a **Web Application** that consumes the RickAndMorty API endpoint.

The web app communicates exclusively with the Api.



<img width="858" height="446" alt="image" src="https://github.com/user-attachments/assets/0a0d31bd-fd0c-4c5e-a81c-812f2be4bf9b" />
