import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def parseBusinessData():
    #read the JSON file
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 1
        outfile.write("HEADER: (business_id, name; address; state; city; postal_code; latitude; longitude; stars; is_open)" '\n')
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(str(count_line) + "- business info: '")
            HelperFunction(data, outfile)
            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close() 
  

def HelperFunction(obj, outfile):
    for i in obj:
        if isinstance(obj[i], dict):
            HelperFunction(obj[i], outfile)
        else:

            outfile.write(i + ", '" + str(obj[i]))
        outfile.write('\n')
          
   
def parseUserData():
    #write code to parse yelp_user.JSON
    with open('yelp_user.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('userData.txt', 'w')
        line = f.readline()
        count_line = 0
        outfile.write("HEADER: (user_id; name; yelping_since; tipcount; fans; average_stars; (funny,useful,cool))" '\n')
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(str(count_line) + "- user info: '")
            HelperFunction(data, outfile)
            line = f.readline()
            count_line +=1
        print(count_line)
        outfile.close()
        f.close()
    pass 

def parseCheckinData():
    #write code to parse yelp_checkin.JSON
    with open('yelp_checkin.JSON', 'r') as f:
        outfile = open('checkin.txt', 'w')
        line = f.readline()
        count_line = 1
        outfile.write("HEADER: (business_id : (year,month,day,time))" '\n')
        while line:
            data = json.loads(line)
            outfile.write(str(count_line) + "- '")
            outfile.write(cleanStr4SQL(data['business_id']) + "':" '\n')
            outfile.write("(")
            date = data['date']

            outfile.write(str(date))
    
            line = f.readline()
            count_line +=1
        print(count_line)
        outfile.close()
        f.close()
    pass


def parseTipData():
    #write code to parse yelp_tip.JSON
    with open('yelp_tip.JSON', 'r') as f:
        outfile = open('tip.txt', 'w')
        line = f.readline()
        count_line = 1
        outfile.write("HEADER: (business_id; date; likes; text; user_id)" '\n')
        while line:
            data = json.loads(line)
            outfile.write(str(count_line) + "-'")

            outfile.write(cleanStr4SQL(data['business_id']) + "' ; '")
            outfile.write(str(data['date']) + "' ; ")
            outfile.write(str(data['likes']) + " ; '")
            outfile.write(str(data['text']) + "' ; '")
            outfile.write(str(data['user_id']) + "'")
            outfile.write('\n')

            line = f.readline()
            count_line +=1
        print(count_line)
        outfile.close()
        f.close()
    pass

parseBusinessData()
parseUserData()
#parseCheckinData()
#parseTipData()
