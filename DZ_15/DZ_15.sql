/* SQL запрос, который вернет названия и описания всех фильмов, продолжительность которых больше 100. */
select title, description
from film
where length > 100;

/* SQL запрос, который вернет уникальные имена (без фамилий) актеров. */
select distinct first_name
from actor;

/* SQL запрос, который вернет рейтинг фильма и количество фильмов с таким рейтингом,
   но только для тех рейтингов, которые содержат букву "G". */
select rating, count(*) count_of_rating
from film
where rating in ('G', 'PG', 'PG-13') -- enumerated type, поэтому задаем конкретные значения
group by rating
order by 2 desc;

/* SQL запрос, который вернет имена и фамилии только тех актеров, которые снимались менее, чем в 20 фильмах. */
select a.first_name, a.last_name, count(fa.film_id) film_count
from actor a
left join film_actor fa on a.actor_id = fa.actor_id
group by a.first_name, a.last_name
having count(fa.film_id) < 20;