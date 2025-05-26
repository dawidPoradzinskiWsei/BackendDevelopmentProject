## User Endpoints

- `GET /api/user/games` — Zwraca wszystkie gry
- `GET /api/user/game/{id}` — Zwraca grę o podanym ID
- `GET /api/user/game/title/{title}` — Zwraca grę po tytule

## Admin Endpoints (JWT, rola Admin)

- `POST /api/admin/game` — Dodaje nową grę
- `PUT /api/admin/game/{id}` — Aktualizuje grę o podanym ID
- `DELETE /api/admin/game/{id}` — Usuwa grę o podanym ID
