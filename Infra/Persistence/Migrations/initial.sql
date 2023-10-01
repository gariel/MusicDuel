create table public.rooms
(
    id              serial
        constraint rooms_pk
            primary key,
    title           text not null,
    is_playing      boolean not null,
    game_type       integer not null,
    visibility_type integer not null,
    rounds          integer not null,
    playlist_code   text
);

alter table public.rooms
    owner to postgres;

create table public.users
(
    id              serial
        constraint users_pk
            primary key,
    name            text,
    hashed_password text
);

alter table public.users
    owner to postgres;

create table public.room_players
(
    room_id   integer
        constraint room_players_room__fk
            references public.rooms,
    user_id   integer
        constraint room_players_user__fk
            references public.users,
    is_owner  boolean,
    entry_num serial
);

alter table public.room_players
    owner to postgres;

