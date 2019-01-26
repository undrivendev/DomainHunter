select 'free' kind, count(*) from "domain" where status = 0
union
select 'taken' kind, count(*) from "domain" where status = 1
union
select 'errors' kind, count(*) from "domain" where status = 2
order by 1