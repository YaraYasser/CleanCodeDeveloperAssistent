import sys
import traceback

import nltk
from nltk.corpus import wordnet
from textblob import TextBlob
if __name__ == '__main__':
    fileName = "D:/ForthYear/GP/Final1.withBugs(3)/21-11-17/NewParserForm/datafile.txt"
    f = open(fileName, 'r')
    s = f.read()
    f.close()
synonyms = []
antonyms = []
# txt = "faculties"
# blob = TextBlob(txt)
# print(blob.noun_phrases)

# essays = u"""education domain system will tell you your grades in all semesters by entering your ID"""
tokens = nltk.word_tokenize(s)
tagged = nltk.pos_tag(tokens)
nouns = [word for word,pos in tagged]

# into_string = str(nouns)
TheRequestedList = []
for i in nouns:
           if i not in TheRequestedList:
            TheRequestedList.append(i)

for i in TheRequestedList:
    for syn in wordnet.synsets(i):
        for l in syn.lemmas():
            synonyms.append(l.name())
            if l.antonyms():
                antonyms.append(l.antonyms()[0].name())

returnedValue1 = set(synonyms)
returnedValue2 = set(antonyms)
strww = repr(returnedValue1)
strr = repr(returnedValue2)
print(strww)
print(strr)