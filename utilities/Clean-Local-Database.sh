#!/bin/bash
sqlCommand=$(<Clean-Database.sql)
psql postgresql://postgres:root@localhost:5432/personal_site_db << EOF
       $sqlCommand
EOF
